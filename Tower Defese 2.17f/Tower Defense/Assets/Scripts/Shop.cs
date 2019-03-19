using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint autocannonTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserbeamTurret;
    public TurretBlueprint plasmaCannonTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;  
    }

    /// <summary>
    /// handles the selection of a Autocannon Turret
    /// passes prefab to build to the buildManager SelectTurrtToBuild function
    /// </summary>
	public void SelectAutocannonTurret()
    {
        Debug.Log("Autocannon Turret Selected");
        buildManager.SelectTurretToBuild(autocannonTurret);
    }

    /// <summary>
    /// handles the selection of a Missile Turret
    /// passes prefab to build to the buildManager SelectTurrtToBuild function
    /// </summary>
    public void SelectMissileTurret()
    {
        Debug.Log("Missile Turret Selected");
        buildManager.SelectTurretToBuild(missileTurret);
    }

    /// <summary>
    /// handles the selection of a Laser Turret
    /// passes prefab to build to the buildManager SelectTurrtToBuild function
    /// </summary>
    public void SelectLaserBeamTurret()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectTurretToBuild(laserbeamTurret);
    }

    /// <summary>
    /// handles the selection of a Plasma Cannon Turret
    /// passes prefab to build to the buildManager SelectTurrtToBuild function
    /// </summary>
    public void SelectPlasmaCannonTurret()
    {
        Debug.Log("plasma Turret Selected");
        buildManager.SelectTurretToBuild(plasmaCannonTurret);
    }
}
