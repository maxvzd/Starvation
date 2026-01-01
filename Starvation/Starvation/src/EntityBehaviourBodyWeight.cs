using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace Starvation;

public class EntityBehaviourBodyWeight(Entity entity) : EntityBehavior(entity)
{
    private float _saturationLastTick = 0f;
    private float _hungerTick = 0f;
    private ITreeAttribute? _bodyWeightTree;

    public override string PropertyName() => ENTITY_KEY;
    public const string ENTITY_KEY = "body-weight";

    public float? BodyWeight
    {
        get => _bodyWeightTree?.GetFloat("weight");
        set
        {
            _bodyWeightTree?.SetFloat("weight", value ?? 0); 
            entity.WatchedAttributes.MarkPathDirty(PropertyName());
        }
    }

    public override void Initialize(EntityProperties properties, JsonObject attributes)
    {
        base.Initialize(properties, attributes);

        var hungerBehaviour = entity.GetBehavior<EntityBehaviorHunger>();
        if (hungerBehaviour is not null)
        {
            _saturationLastTick = hungerBehaviour.Saturation;
        }
        
        _bodyWeightTree = entity.WatchedAttributes.GetTreeAttribute(PropertyName());
        if (_bodyWeightTree is null)
        {
            entity.WatchedAttributes.SetAttribute(PropertyName(), _bodyWeightTree = new TreeAttribute());
        } 
    }

    public override void OnGameTick(float deltaTime)
    {
        if (entity.World.Side != EnumAppSide.Server) return;

        _hungerTick += deltaTime;
        if (_hungerTick > 10)
        {
            var hungerBehaviour = entity.GetBehavior<EntityBehaviorHunger>();
            if (hungerBehaviour is null) return;
            
            var satDiff = _saturationLastTick - hungerBehaviour.Saturation;
            if (satDiff > 0)
            {
                BodyWeight += satDiff;
            }
            
            Globals.CoreApiInstance?.Logger.Debug($"_saturationLastTick: {_saturationLastTick}, currSat: {hungerBehaviour.Saturation}, satDiff: {satDiff}, BodyWeight: {BodyWeight}");


            _saturationLastTick = hungerBehaviour.Saturation;
            _hungerTick = 0f;
        }
    }
}