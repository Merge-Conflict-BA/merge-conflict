/**********************************************************************************************************************
Name:          ComponentMerger
Description:   Merges the two given gameobject and returns a new one.
Author(s):     Daniel Rittrich
Date:          2024-03-07
Version:       V1.0
TODO:          - 
**********************************************************************************************************************/

#nullable enable
using ConveyorBelt;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComponentMerger : MonoBehaviour
{
    private static ComponentMerger _instance;
    public static ComponentMerger Instance { get { return _instance; } }

    public GameObject objectToSpawnAfterMerge;

    void Awake()
    {
        //singleton -> only 1 instance
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    /*
     * Usage of Components:
     * 
     * CPUComponent cpu = Components.CPU;
     * cpu.level = 3;
     * 
     * MBComponent motherboard = Components.CreateMB();
     * MBComponent motherboardWithCPU = Components.CreateMB(cpu: cpu);
     * 
     * Trash trash = Components.CreateTrash(TrashVariant.Can);
     * Trash trashRandom = Components.CreateTrash();
     */

    public Element InstantiateNewClass(Element element)
    {

        var (trashValue, salesValue) = ComponentValues.GetComponentValues(element);

        switch (element)
        {
            case CaseComponent:
                CaseComponent caseComp = Components.CreateCase(powersupply: element.GetComponent<PowersupplyComponent>(), hdd: element.GetComponent<HDDComponent>(), motherboard: element.GetComponent<MBComponent>());
                caseComp.level = element.level;
                caseComp.trashValue = trashValue;
                caseComp.salesValue = salesValue;
                return caseComp;

            case PowersupplyComponent:
                PowersupplyComponent powersupply = Components.Powersupply;
                powersupply.level = element.level;
                powersupply.trashValue = trashValue;
                powersupply.salesValue = salesValue;
                return powersupply;

            case HDDComponent:
                HDDComponent hdd = Components.HDD;
                hdd.level = element.level;
                hdd.trashValue = trashValue;
                hdd.salesValue = salesValue;
                return hdd;

            case MBComponent:
                MBComponent mb = Components.CreateMB(cpu: element.GetComponent<CPUComponent>(), ram: element.GetComponent<RAMComponent>(), gpu: element.GetComponent<GPUComponent>());
                mb.level = element.level;
                mb.trashValue = trashValue;
                mb.salesValue = salesValue;
                return mb;

            case CPUComponent:
                CPUComponent cpu = Components.CPU;
                cpu.level = element.level;
                cpu.trashValue = trashValue;
                cpu.salesValue = salesValue;
                return cpu;

            case RAMComponent:
                RAMComponent ram = Components.RAM;
                ram.level = element.level;
                ram.trashValue = trashValue;
                ram.salesValue = salesValue;
                return ram;

            case GPUComponent:
                GPUComponent gpu = Components.GPU;
                gpu.level = element.level;
                gpu.trashValue = trashValue;
                gpu.salesValue = salesValue;
                return gpu;

            default:
                Debugger.LogError("Instantiating class not possible. Returning old element instead.");
                return element;
        }
    }

    public GameObject? Merge(GameObject staticObj, GameObject draggedObj)
    {
        var mergeResult = staticObj.GetComponent<IComponent>().Merge(draggedObj.GetComponent<Element>());
        if (mergeResult == null)
        {
            mergeResult = draggedObj.GetComponent<IComponent>().Merge(staticObj.GetComponent<Element>());
            if (mergeResult == null)
            {
                Debug.Log(" - Components not mergable - ");
            }
            else
            {
                Debug.Log($" - Components {staticObj.GetComponent<Element>()} and {draggedObj.GetComponent<Element>()} are mergable - ");
            }
        }
        if (mergeResult != null)
        {
            // TODO Daniel : hier weiter machen

            // Element newClassComponent = InstantiateNewClass(mergeResult);
            // objectToSpawnAfterMerge.AddComponent<newClassComponent>();
            return objectToSpawnAfterMerge;
        }

        return null;
    }
}
