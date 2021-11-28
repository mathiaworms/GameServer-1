using System.Linq;
using GameServerCore;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using System.Numerics;
using LeagueSandbox.GameServer.API;
using System.Collections.Generic;
using GameServerCore.Scripting.CSharp;
using GameServerCore.Domain.GameObjects.Spell.Sector;
using GameServerCore.Scripting.CSharp;
using System;

namespace Spells
{
    public class TriumphantRoar : ISpellScript
    {
        public ISpellScriptMetadata ScriptMetadata => new SpellScriptMetadata()
        {

        };

        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
           
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
        }

        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
            IChampion mostWoundedAlliedIChampion = null;

            if (target != null
                && target is IChampion ch
                && IsUnitInRange(ch, owner.Position, spell.SpellData.CastRangeDisplayOverride, true))
            {
                mostWoundedAlliedIChampion = ch;
            }

            if (mostWoundedAlliedIChampion == null)
            {
                var units = GetChampionsInRange(owner.Position, spell.SpellData.CastRangeDisplayOverride, true);
                float lowestHealthPercentage = 100;
                float maxHealth;
                foreach (var value in units)
                {
                    if (value != owner && owner.Team == value.Team)
                    {
                        var currentHealth = value.Stats.CurrentHealth;
                        maxHealth = value.Stats.HealthPoints.Total;
                        if (currentHealth * 100 / maxHealth < lowestHealthPercentage && owner != value)
                        {
                            lowestHealthPercentage = currentHealth * 100 / maxHealth;
                            mostWoundedAlliedIChampion = value;
                        }
                    }
                }
            }

            if (mostWoundedAlliedIChampion != null)
            {
                PerformHeal(owner, spell, mostWoundedAlliedIChampion);
            }

            PerformHeal(owner, spell, owner);

        }

        public void OnSpellCast(ISpell spell)
        {
            
        }

        public void OnSpellPostCast(ISpell spell)
        {
           
        }
       private void PerformHeal(IObjAiBase owner, ISpell spell, IAttackableUnit target)
        {

            var ap = owner.Stats.AbilityPower.Total * spell.SpellData.MagicDamageCoefficient;
            float healthGain = 20 + (spell.CastInfo.SpellLevel * 40) + ap;
            if (target.HasBuff("HealCheck"))
            {
                healthGain *= 0.5f;
            }
            var newHealth = target.Stats.CurrentHealth + healthGain;
            target.Stats.CurrentHealth = Math.Min(newHealth, target.Stats.HealthPoints.Total);

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
