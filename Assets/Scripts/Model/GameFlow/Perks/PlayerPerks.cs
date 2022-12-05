using UnityEngine;

public abstract class PlayerPerk
{
    [field: SerializeField] public int BuyPerkCost { get; private set; }

    public abstract bool PerkAvaliable();
}
