﻿using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using GameServerCore.Enums;
using GameServerCore.Scripting.CSharp;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
namespace Buffs
{
    internal class DravenSpinning : IBuffGameScript
    {
        public BuffType BuffType => BuffType.COMBAT_ENCHANCER;
        public BuffAddType BuffAddType => BuffAddType.STACKS_AND_OVERLAPS;
        public int MaxStacks => 2;
        public bool IsHidden => false;

        public IStatsModifier StatsModifier { get; private set; } = new StatsModifier();

        IObjAiBase _owner;

        public void OnActivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            _owner = unit as IObjAiBase;
            ApiEventManager.OnHitUnit.AddListener(this, _owner, TargetExecute, true);
            //PlayAnimation(unit, "SPELL3A");
        }

        private void TargetExecute(IAttackableUnit Unit, bool crit)
        {
            Unit.TakeDamage(_owner, _owner.Stats.AttackDamage.Total, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
        }

        public void OnDeactivate(IAttackableUnit unit, IBuff buff, ISpell ownerSpell)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this, _owner);
        }

        public void OnUpdate(float diff)
        {
        }
    }
}