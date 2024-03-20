using UnityEngine;
using Zenject;

public class MainSceneInjector : MonoInstaller
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterRotation _characterRotation;
    [SerializeField] private Hand _hand;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EnemyMaster _enemyMaster;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private GameFinisher _gameFinisher;

    public override void InstallBindings()
    {
        NewControls instance = Container.Instantiate<NewControls>();
        instance.Enable();

        Container.Bind<NewControls>().FromInstance(instance).AsSingle().NonLazy();
        Container.Bind<PlayerScore>().FromNew().AsSingle().NonLazy();
        Container.Bind<EnemyMaster>().FromInstance(_enemyMaster).AsSingle().NonLazy();
        Container.InjectGameObject(_gameFinisher.gameObject);
        Container.InjectGameObject(_characterMovement.gameObject);
        Container.InjectGameObject(_characterRotation.gameObject);
        Container.InjectGameObject(_scoreManager.gameObject);
        Container.InjectGameObject(_hand.gameObject);
        Container.InjectGameObject(_scoreView.gameObject);
    }
}