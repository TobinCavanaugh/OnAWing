using DefaultNamespace;
using UnityEngine;

public class SpeedLines : MonoBehaviour
{
    
    public float divisionFac = 4.5f;

    public ParticleSystem particleSystem;
    public SplineBasedBirdController splineBasedBirdController;

    private ParticleSystem.EmissionModule _emissionModule;
    private ParticleSystem.MinMaxCurve _minMaxCurve;
    private void Start()
    {
        _emissionModule = particleSystem.emission;
        _minMaxCurve = _emissionModule.rateOverTime;
    }

    // Update is called once per frame
    void Update()
    {
        _minMaxCurve.constant = splineBasedBirdController.curMoveSpeed / divisionFac;
        _emissionModule.rateOverTime = _minMaxCurve;
    }
}
