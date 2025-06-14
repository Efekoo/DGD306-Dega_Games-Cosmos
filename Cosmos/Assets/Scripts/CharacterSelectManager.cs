using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CharacterSelectManager : MonoBehaviour
{
    public GameObject highlightP1;
    public Transform[] characterPositions;

    private int currentSelection = 0;

    private PlayerInput _playerInput;
    private InputAction _navigate;
    private InputAction _select;
    private InputAction _soloPlay;
    private InputAction _coopPlay;


    
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        var actionMap = _playerInput.actions.FindActionMap("PlayerSelect");

        _navigate = actionMap.FindAction("Navigate");
        _select = actionMap.FindAction("Select");
        _soloPlay = actionMap.FindAction("SoloPlay");
        _coopPlay = actionMap.FindAction("CoopPlay");

        _select.performed += ctx => OnSelect();
        _soloPlay.performed += ctx => OnSoloPlay();
        _coopPlay.performed += ctx => OnCoopPlay();

        highlightP1.SetActive(true);
        UpdateHighlightPosition();
    }
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(GameObject.Find("p1buton"));
    }
    public void SelectP1()
    {
        PlayerSelectionData.player1Index = 0;
        highlightP1.SetActive(true);
        highlightP1.transform.position = characterPositions[0].position;
        Debug.Log("Player 1 seçimi: Gemi 1");
    }

    public void SelectP2()
    {
        PlayerSelectionData.player1Index = 1;
        highlightP1.SetActive(true);
        highlightP1.transform.position = characterPositions[1].position;
        Debug.Log("Player 1 seçimi: Gemi 2");
    }

    void OnEnable()
    {
        _navigate.performed += OnNavigate;
    }

    void OnDisable()
    {
        _navigate.performed -= OnNavigate;
    }

    void OnNavigate(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        Debug.Log("Navigate input: " + dir);

        if (dir.x > 0.5f)
            currentSelection = 1;
        else if (dir.x < -0.5f)
            currentSelection = 0;

        UpdateHighlightPosition();
    }

    void UpdateHighlightPosition()
    {
        highlightP1.transform.position = characterPositions[currentSelection].position;
    }

    void OnSelect()
    {
        PlayerSelectionData.player1Index = currentSelection;
        Debug.Log("Player seçimi yapıldı: " + currentSelection);
    }

    void OnSoloPlay()
    {
        if (PlayerSelectionData.player1Index != -1)
        {
            PlayerSelectionData.isCoop = false;
            PlayerSelectionData.player2Index = -1;
            SceneManager.LoadScene("Tutorial");
        }
    }

    void OnCoopPlay()
    {
        if (PlayerSelectionData.player1Index != -1)
        {
            PlayerSelectionData.isCoop = true;
            PlayerSelectionData.player2Index = (PlayerSelectionData.player1Index == 0) ? 1 : 0;
            SceneManager.LoadScene("Tutorial");
        }
    }
}