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

    public static GameObject objectToSpawnAfterMerge;


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

    private bool CaseHasComponents(Element element)
    {
        MBComponent? motherboard = element.GetComponent<MBComponent>();
        PowersupplyComponent? powersupply = element.GetComponent<PowersupplyComponent>();
        HDDComponent? hdd = element.GetComponent<HDDComponent>();

        return motherboard || powersupply || hdd;
    }


    public static Element InstantiateNewClass(Element element)
    {

        var (trashValue, salesValue) = ComponentValues.GetComponentValues(element);

        switch (element)
        {
            case CaseComponent:
                PowersupplyComponent? powersupplyForCase = null;
                HDDComponent? hddForCase = null;
                MBComponent? mbForCase = null;

                //case has powersupply
                if (element.GetComponent<PowersupplyComponent>() != null)
                {
                    powersupplyForCase = Components.Powersupply;
                    powersupplyForCase.level = element.GetComponent<PowersupplyComponent>().level;
                    var (trashValueCaseWithPowersupply, salesValueCaseWithPowersupply) = ComponentValues.GetComponentValues(element.GetComponent<PowersupplyComponent>());
                    powersupplyForCase.trashValue = trashValueCaseWithPowersupply;
                    powersupplyForCase.salesValue = salesValueCaseWithPowersupply;
                }

                //case has hdd
                if (element.GetComponent<HDDComponent>() != null)
                {
                    hddForCase = Components.HDD;
                    hddForCase.level = element.GetComponent<HDDComponent>().level;
                    var (trashValueHdd, salesValueHdd) = ComponentValues.GetComponentValues(element.GetComponent<HDDComponent>());
                    hddForCase.trashValue = trashValueHdd;
                    hddForCase.salesValue = salesValueHdd;
                }

                //case has motherboard
                if (element.GetComponent<MBComponent>() != null) { mbForCase = InstantiateMotherboard(element); };

                CaseComponent caseComp = Components.CreateCase(powersupply: powersupplyForCase, hdd: hddForCase, motherboard: mbForCase);
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

    //create an instance from motherboard
    //depeds on the environment of the motherboard --> motherboard can be completed into an case or standalon
    private static MBComponent InstantiateMotherboard(Element element)
    {
        CPUComponent? cpuForMb = null;
        RAMComponent? ramForMb = null;
        GPUComponent? gpuForMb = null;
        MBComponent? motherboardInstance = null;

        bool isCase = element is CaseComponent;
        bool isMotherboard = element is MBComponent;

        //case has motherboard and motherboard has cpu || motherboard has CPU 
        if ((isCase && (element.GetComponent<MBComponent>().GetComponent<CPUComponent>() != null)) ||
            (isMotherboard && (element.GetComponent<CPUComponent>() != null)))
        {
            cpuForMb = Components.CPU;
            cpuForMb.level = element.GetComponent<CPUComponent>().level;
            var (trashValueCpu, salesValueCpu) = isCase ? ComponentValues.GetComponentValues(element.GetComponent<MBComponent>().GetComponent<CPUComponent>()) : ComponentValues.GetComponentValues(element.GetComponent<CPUComponent>());
            cpuForMb.trashValue = trashValueCpu;
            cpuForMb.salesValue = salesValueCpu;
        }

        //case has motherboard and motherboard has ram || motherboard has RAM
        if ((isCase && (element.GetComponent<MBComponent>().GetComponent<RAMComponent>() != null)) ||
            (isMotherboard && (element.GetComponent<RAMComponent>() != null)))

        {
            ramForMb = Components.RAM;
            ramForMb.level = element.GetComponent<MBComponent>().GetComponent<CPUComponent>().level;
            var (trashValueRam, salesValueRam) = isCase ? ComponentValues.GetComponentValues(element.GetComponent<MBComponent>().GetComponent<CPUComponent>()) : ComponentValues.GetComponentValues(element.GetComponent<RAMComponent>());
            ramForMb.trashValue = trashValueRam;
            ramForMb.salesValue = salesValueRam;
        }

        //case has motherboard and motherboard has gpu || motherboard has gpu
        if (isCase && (element.GetComponent<MBComponent>().GetComponent<GPUComponent>() != null) ||
            (isMotherboard && (element.GetComponent<GPUComponent>() != null)))
        {
            gpuForMb = Components.GPU;
            gpuForMb.level = element.GetComponent<MBComponent>().GetComponent<GPUComponent>().level;
            var (trashValueGpu, salesValueGpu) = isCase ? ComponentValues.GetComponentValues(element.GetComponent<MBComponent>().GetComponent<GPUComponent>()) : ComponentValues.GetComponentValues(element.GetComponent<GPUComponent>());
            gpuForMb.trashValue = trashValueGpu;
            gpuForMb.salesValue = salesValueGpu;
        }

        motherboardInstance = Components.CreateMB(cpu: cpuForMb, ram: ramForMb, gpu: gpuForMb);
        motherboardInstance.level = element.GetComponent<MBComponent>().level;
        var (trashValueMb, salesValueMb) = ComponentValues.GetComponentValues(element.GetComponent<MBComponent>());
        motherboardInstance.trashValue = trashValueMb;
        motherboardInstance.salesValue = salesValueMb;

        return motherboardInstance;
    }


    public static GameObject? Merge(GameObject staticObj, GameObject draggedObj)
    {
        var mergeResult = staticObj.GetComponent<IComponent>().Merge(draggedObj.GetComponent<Element>());
        if (mergeResult == null)
        {
            mergeResult = draggedObj.GetComponent<IComponent>().Merge(staticObj.GetComponent<Element>());
            if (mergeResult == null)
            {
                Debugger.LogMessage(" - Components not mergable - ");
            }
            else
            {
                Debugger.LogMessage($" - Components {staticObj.GetComponent<Element>()} and {draggedObj.GetComponent<Element>()} are mergable - ");
            }
        }
        if (mergeResult != null)
        {
            // TODO Daniel : hier weiter machen

            Element newClassComponent = InstantiateNewClass(mergeResult);
            objectToSpawnAfterMerge.AddComponent(newClassComponent.GetType());

            Debugger.LogMessage("spawn this: " + newClassComponent);
            Debugger.LogMessage("from type: " + newClassComponent.GetType());

            return objectToSpawnAfterMerge;  // dieses noch instanziieren
        }

        return null;
    }
}
