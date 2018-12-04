using UnityEngine;

public class TeleportationSkill : Skill
{
    // Player transform
    public Transform player;
    // Target prefab
    private GameObject targetPrefab;
    // Target the player will be teleported to
    private GameObject target;
    // Target opacity
    private readonly float opacity = 0.2f;

    new void Awake()
    {
        base.Awake();
        cooldown = 1f; // 1 second for debug only
        targetPrefab = Resources.Load<GameObject>("Prefabs/Player/Skills/TeleportationTarget");
        player = GetComponent<Transform>();
    }

    void Update()
    {
        if (state == SkillState.Casting && Input.GetMouseButtonDown(0))
            Deactivate();
    }

    // Creates a player-like target that follows mouse cursor
    protected override void DoActivate()
    {
        state = SkillState.Casting;
        target = Instantiate(targetPrefab, Input.mousePosition, Quaternion.identity);

        SpriteRenderer targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        targetSpriteRenderer.sprite = player.Find("Body").GetComponent<SpriteRenderer>().sprite;
        targetSpriteRenderer.color = new Color(1f, 1f, 1f, opacity);        
    }

    // Teleports player to the target
    protected override void DoDeactivate()
    {
        player.position = target.GetComponent<Transform>().position;
        Destroy(target);
    }

    protected override void Interrupt()
    {
        Destroy(target);
    }

}
