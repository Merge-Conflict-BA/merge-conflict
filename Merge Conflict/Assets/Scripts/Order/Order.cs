using Unity.VisualScripting;

public class Order
{
    public int CaseTier;
    public int HddTier;
    public int PowersupplyTier;
    public int MotherboardTier;
    public int GpuTier;
    public int CpuTier;
    public int RamTier;

    public Order(int caseTier, int hddTier, int powersupplyTier, int motherboardTier, int gpuTier, int cpuTier, int ramTier)
    {
        CaseTier = caseTier;
        HddTier = hddTier;
        PowersupplyTier = powersupplyTier;
        MotherboardTier = motherboardTier;
        GpuTier = gpuTier;
        CpuTier = cpuTier;
        RamTier = ramTier;
    }

    public static Order EmptyOrder()
    {
        return new Order(0,0,0,0,0,0,0);
    }


}