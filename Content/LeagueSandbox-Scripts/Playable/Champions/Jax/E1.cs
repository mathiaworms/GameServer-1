using System.Collections.Generic;
using System.Numerics;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain.GameObjects.Spell;
using GameServerCore.Domain.GameObjects.Spell.Missile;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Scripting.CSharp;

namespace Spells
{
    public class JaxCounterStrike: ISpellScript
    {
        IObjAiBase Owner;
        public ISpellScriptMetadata ScriptMetadata { get; private set; } = new SpellScriptMetadata()
        {
            TriggersSpellCasts = true,
        };

        public void OnActivate(IObjAiBase owner, ISpell spell)
        {
            Owner = owner;
        }

        public void OnDeactivate(IObjAiBase owner, ISpell spell)
        {
        }
        static internal IParticle x;
        public void OnSpellPreCast(IObjAiBase owner, ISpell spell, IAttackableUnit target, Vector2 start, Vector2 end)
        {
        }

        public void OnSpellCast(ISpell spell)
        {
            var owner = spell.CastInfo.Owner;
            PlayAnimation(owner, "Spell3", 2.0f);
            AddBuff("JaxCounterStrikeAttack", 5f, 1, spell, Owner, Owner, false);
            CreateTimer(0.1f, () => { owner.Spells[2].SetCooldown((float)0.1); });
            //CreateTimer(2.f, () => { owner.Spells[2].Cast(owner.Position, owner.Position); });
            //CreateTimer(3.0f, () => { owner.SetSpell("JaxCounterStrike", 2, true); });
            //CreateTimer(3.0f, () => { owner.Spells[2].SetCooldown((float)4.1); });
            x = AddParticleTarget(owner, owner, "JaxDodger.troy", owner, 5f);
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