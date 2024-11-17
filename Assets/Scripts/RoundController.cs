using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    public PhysisManager physisManager { get; set; }
    public CCActionManager ccActionManager { get; set; }

    ScoreController scoreController;
    DiskFactory diskFactory = DiskFactory.GetInstance();

    private int round = 1;
    private bool game_quit = false;
    private int haven_throw = 0;
    private float throw_interval = 1.5f;

    public Text RoundText;
    public Text ScoreText;
    public GameObject GameOverTip;

    void Awake()
    {
        SSDirector director = SSDirector.GetInstance();
        director.setFPS(60);
        director.roundController = this;
    }

    public int GetRound()
    {
        return round;
    }
    public void NextRound()
    {
        round += 1;
        RoundText.text = "Round: " + round.ToString();
        DiskFactory diskFactory = DiskFactory.GetInstance();
        diskFactory.Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreController = new ScoreController();
    }

    // Update is called once per frame
    void Update()
    {
        throw_interval -= Time.deltaTime;
        if (throw_interval <= 0)
        {
            if (game_quit)
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
            throw_interval = 1.5f;
            haven_throw += 1;
            if (haven_throw > 10)
            {
                if (round == 10)
                {
                    game_quit = true;
                    return;
                }
                NextRound();
                haven_throw = 1;
            }

            int times = Random.Range(1, 4);
            if (SSDirector.GetInstance().gameMode == 0)
            {
                while (times-- > 0)
                {
                    physisManager.PlayDisk(DiskFactory.GetInstance().GetDisk(round, 0));
                }
            }
            else
            {
                while(times-- > 0)
                {
                    ccActionManager.PlayDisk(DiskFactory.GetInstance().GetDisk(round, 1));
                }
            }
        }

        if (Input.GetMouseButtonDown(0))  // 0 for left-click
        {
            // Creates a ray that emits from the camera to the location of the mouse pointer
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Detect the collision of the ray with an object in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the colliding object is the current object
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag.Contains("Disk"))
                    {
                        Debug.Log("Hit UFO!");
                        this.scoreController.RecordDisk(hit.collider.gameObject);
                        hit.collider.gameObject.SetActive(false);
                        ScoreText.text = "Scores: " + scoreController.GetScore().ToString();
                    }
                    if (hit.collider.gameObject.tag.Contains("Finish"))
                    {
                        Debug.Log("Game Over!");
                        game_quit = true;
                        GameOverTip.SetActive(true);
                    }
                }
            }
        }
    }

}
