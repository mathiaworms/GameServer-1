using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using System.Numerics;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain;
using GameServerLib.GameObjects.AttackableUnits;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;


namespace Passives
{
     
    public class JaxPassive : ICharScript

    {
        ISpell Spell;
        public void OnActivate(IObjAiBase owner, ISpell spell)

        {

            Spell = spell;


            {
                ApiEventManager.OnHitUnit.AddListener(this, owner, OnHitUnit, false);
            }
        }
        public void OnHitUnit(IAttackableUnit target, bool IsCrit)

        
        {
            var owner = Spell.CastInfo.Owner;
            AddBuff("JaxPassive", 2.5f, 1, Spell, owner, owner);

        }
        


 
        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this);
        }
        public void OnUpdate(float diff)
        {
        }
    }
}