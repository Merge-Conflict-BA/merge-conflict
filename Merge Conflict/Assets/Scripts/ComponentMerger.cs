/**********************************************************************************************************************
Name:          ComponentMerger
Description:   Merges the two given gameobject and returns a new one.
Author(s):     Daniel Rittrich, Markus Haubold
Date:          2024-03-07
Version:       V1.2
TODO:          - 
**********************************************************************************************************************/

using System.Collections.Generic;
using UnityEngine;

public class ComponentMerger : MonoBehaviour
{
    private static ComponentMerger _instance;
    public static ComponentMerger Instance { get { return _instance; } }

    public GameObject objectToSpawnAfterMerge;
    public static GameObject ObjectToSpawnAfterMerge { get; private set; }
    public GameObject emptyObjectToSpawnAfterMerge;
    public static GameObject EmptyObjectToSpawnAfterMerge { get; private set; }


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

        ObjectToSpawnAfterMerge = objectToSpawnAfterMerge;
        EmptyObjectToSpawnAfterMerge = emptyObjectToSpawnAfterMerge;
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

#nullable enable

    public static Element InstantiateNewClass(Element element)
    {

        var (trashValue, salesValue) = ComponentValues.GetComponentValues(element);

        int totalTrashValueNewClass = 0;
        int totalSalesValueNewClass = 0;

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
                    totalTrashValueNewClass += trashValueCaseWithPowersupply;
                    totalSalesValueNewClass += salesValueCaseWithPowersupply;
                }

                //case has hdd
                if (element.GetComponent<HDDComponent>() != null)
                {
                    hddForCase = Components.HDD;
                    hddForCase.level = element.GetComponent<HDDComponent>().level;
                    var (trashValueHdd, salesValueHdd) = ComponentValues.GetComponentValues(element.GetComponent<HDDComponent>());
                    hddForCase.trashValue = trashValueHdd;
                    hddForCase.salesValue = salesValueHdd;
                    totalTrashValueNewClass += trashValueHdd;
                    totalSalesValueNewClass += salesValueHdd;
                }

                //case has motherboard
                if (element.GetComponent<MBComponent>() != null) { mbForCase = InstantiateMotherboard(element); };

                CaseComponent caseComp = Components.CreateCase(powersupply: powersupplyForCase, hdd: hddForCase, motherboard: mbForCase);
                caseComp.level = element.level;
                caseComp.trashValue = trashValue + totalTrashValueNewClass + (mbForCase != null ? mbForCase.trashValue : 0);
                caseComp.salesValue = salesValue + totalSalesValueNewClass + (mbForCase != null ? mbForCase.salesValue : 0);
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
                MBComponent mb = InstantiateMotherboard(element);
                mb.level = element.level;
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

        int totalTrashValueMB = 0;
        int totalSalesValueMB = 0;

        //case has motherboard and motherboard has cpu || motherboard has CPU 
        if ((isCase && (element.GetComponent<MBComponent>().GetComponent<CPUComponent>() != null)) ||
            (isMotherboard && (element.GetComponent<CPUComponent>() != null)))
        {
            cpuForMb = Components.CPU;
            cpuForMb.level = element.GetComponent<CPUComponent>().level;
            var (trashValueCpu, salesValueCpu) = isCase ? ComponentValues.GetComponentValues(element.GetComponent<MBComponent>().GetComponent<CPUComponent>()) : ComponentValues.GetComponentValues(element.GetComponent<CPUComponent>());
            cpuForMb.trashValue = trashValueCpu;
            cpuForMb.salesValue = salesValueCpu;
            totalTrashValueMB += trashValueCpu;
            totalSalesValueMB += salesValueCpu;
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
            totalTrashValueMB += trashValueRam;
            totalSalesValueMB += salesValueRam;
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
            totalTrashValueMB += trashValueGpu;
            totalSalesValueMB += salesValueGpu;
        }

        motherboardInstance = Components.CreateMB(cpu: cpuForMb, ram: ramForMb, gpu: gpuForMb);
        motherboardInstance.level = element.GetComponent<MBComponent>().level;
        var (trashValueMb, salesValueMb) = ComponentValues.GetComponentValues(element.GetComponent<MBComponent>());
        motherboardInstance.trashValue = trashValueMb + totalTrashValueMB;
        motherboardInstance.salesValue = salesValueMb + totalSalesValueMB;

        return motherboardInstance;
    }


    public static GameObject InstantiateGameObjectAndAddTexture(Element element, Vector2 position)
    {
        // instantiate new GameObject from prefab
        GameObject newObject = Instantiate(ObjectToSpawnAfterMerge, position, Quaternion.Euler(0, 0, 0));
        newObject.name = $"{element.GetType()}_lvl_{element.level}_merged";
        newObject.tag = "Component";
        newObject.AddComponent(element.GetType());

        // get texture for main component and add it to the new GameObject
        ElementTexture newObjectTexture = TextureAtlas.Instance.GetComponentTexture(element);
        newObject.GetComponent<SpriteRenderer>().sprite = newObjectTexture.elementSprite;
        newObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        newObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newObjectTexture.sizeWidth, newObjectTexture.sizeHeight);
        newObject.GetComponent<RectTransform>().localScale = new Vector2(newObjectTexture.sizeScaleX, newObjectTexture.sizeScaleY);
        newObject.GetComponent<BoxCollider2D>().isTrigger = true;
        newObject.GetComponent<BoxCollider2D>().size = new Vector2(newObjectTexture.sizeWidth, newObjectTexture.sizeHeight);

        // check whether the element has subcomponents
        List<ElementTexture> listOfSlotComponentTextures = TextureAtlas.Instance.GetSlotComponentTextures(element);
        if (listOfSlotComponentTextures.Count > 0)
        {
            // instantiate a child GameObject for each subcomponent in the element to layer its sprite over the texture of the main element
            foreach (ElementTexture slotTexture in listOfSlotComponentTextures)
            {
                GameObject newChildObject = Instantiate(EmptyObjectToSpawnAfterMerge, position, Quaternion.Euler(0, 0, 0), newObject.transform.parent);
                newChildObject.transform.SetParent(newObject.transform, true);
                newChildObject.name = $"{element.GetType()}_child";
                newChildObject.GetComponent<SpriteRenderer>().sortingOrder = newObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                newChildObject.GetComponent<SpriteRenderer>().sprite = slotTexture.elementSprite;
                newChildObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                newChildObject.GetComponent<RectTransform>().sizeDelta = new Vector2(slotTexture.sizeWidth, slotTexture.sizeHeight);
                newChildObject.GetComponent<RectTransform>().localScale = new Vector2(slotTexture.sizeScaleX, slotTexture.sizeScaleY);
            }
        }

        return newObject;
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
            // instantiate the new class
            Element newClassComponent = InstantiateNewClass(mergeResult);

            // TODO : set the correct spawn position in this function if I get it wrong   -Daniel-
            // instantiate the new GameObject, add the texture to it, add child objects to it in case it has subcomponents
            GameObject finalGameObjectAfterInstantiationAndTexturesWereAdded = InstantiateGameObjectAndAddTexture(newClassComponent, staticObj.GetComponent<RectTransform>().position);

            Debugger.LogMessage("spawn this: " + newClassComponent);
            Debugger.LogMessage("from type: " + newClassComponent.GetType());

            return finalGameObjectAfterInstantiationAndTexturesWereAdded;
        }

        return null;
    }
}
