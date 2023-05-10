public enum EventName
{
    ExitScene,EnterScene,
    BallHit,BallDead,PrepareDistoryBall,
    ProjectileHit,
    LoadLevel,
    CanShoot,ShootCountInit,
    EnterBulletTime,ExitBulletTime,
    GamePass,GameOver,
    ChangeScale,ChangeShootType
}
public enum PoolName
{
    ProjectilePool,FlashPool,PiecesPool
}
public enum AudioName
{
    Hit_1,Hit_2,BulletTime,BulletHit_1,BulletHit_2,GamePass,BGM
}
public enum GameState
{
    Pause,Gameplay

}
public enum ShootType
{
    Normal,Penetration
}
