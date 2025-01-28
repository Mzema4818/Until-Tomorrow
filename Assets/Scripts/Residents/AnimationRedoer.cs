using UnityEngine;

public class AnimationRedoer : StateMachineBehaviour
{
    public LumberWorker lumberWorker;
    public Lumbermill lumbermill;

    public GameObject resident;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            resident = animator.gameObject;
            lumberWorker = resident.GetComponent<LumberWorker>();
            lumbermill = lumberWorker.lumbermill;

            lumberWorker.ShouldChopTree = false;

            resident.GetComponent<ResidentTools>().ChangeEnable(0, false);
            resident.GetComponent<ResidentTools>().ChangeEnable(1, true);

            Destroy(lumberWorker.Tree.transform.parent.gameObject);
        }
        catch { };
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            animator.SetBool("Chopping", false);
            //THIS 2 IN THE VECTOR 3 IS BECAUSE WHEN YOU SPAWN TREES IN THEY SPAWN -2 IN THE GROUND.  ITS TO COUNTERACT THAT AND MAKE IT EVEN
            GameObject sapling = Instantiate(lumbermill.SaplingPrefab, lumberWorker.TreeLocation, lumberWorker.TreeRotation, lumbermill.saplingParent.transform);
            //sapling.AddComponent<RegrowSaplings>().forestGenerator = lumbermill.forestGenerator;
            //sapling.GetComponent<RegrowSaplings>().parent = lumbermill.treeParent.gameObject;

            lumberWorker.ShouldDropOffTree = true;

            resident.GetComponent<ResidentTools>().ChangeEnable(2, true);
            resident.GetComponent<Animator>().SetBool("Holding", true);
        }
        catch { };
    }
}