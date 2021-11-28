﻿using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using GameServerCore.Enums;
using System.Numerics;
using LeagueSandbox.GameServer.API;
using System.Collections.Generic;
using GameServerCore.Scripting.CSharp;


namespace Spells
{

    public class HextechGunblade : ISpellScript
    {
        IBuff buff;

        public ISpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            MissileParameters = new MissileParameters
            {
                Type = MissileType.Target
            },
            //IsDamagingSpell = true,
            //TriggersSpellCasts = true

        };


        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
            ApiEventManager.OnSpellMissileHit.AddListener(this, new KeyValuePair<ISpell, IObjAiBase>(spell, owner), TargetExecute, false);

            buff = AddBuff("HextechGunbladePassive", 1f, 1, spell, owner, owner, true);
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
            RemoveBuff(buff);
        }

        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
        }

        public void TargetExecute(ISpell spell, IAttackableUnit target, ISpellMissile missile)
        {
            var owner = spell.CastInfo.Owner;
            var APratio = owner.Stats.AbilityPower.Total * 0.4f;
            var damage = 150f + APratio;
            

            AddParticleTarget(owner, owner, "hextech_gunBlade_tar.troy", target, 1f);
            AddBuff("HextechGunblade", 2f, 1, spell, target, owner);

            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

            if (target.IsDead)
            {
                AddParticle(owner, owner, "hextech_gunBlade_tar.troy", target.Position, lifetime: 1f);
            }
            missile.SetToRemove();

        }

        public void OnSpellCast(ISpell spell)
        {
        }

        public void OnSpellPostCast(ISpell spell)
        {
        }

        public void OnSpellChannel(ISpell spell)
        {
        }

        public void OnSpellChannelCancel(ISpell spell)
        {
        }

        public void OnSpellPostChannel(ISpell spell)
        {
        }

        public void OnUpdate(float diff)
        {
        }
    }
}