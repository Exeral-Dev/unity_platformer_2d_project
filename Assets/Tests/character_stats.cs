using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class character_stats
    {
      
        [Test]
        public void character_check_max_health()
        {
            CharacterStats character = new CharacterStats();

            int characterMaxHealth = character.getCharacterMaxHealth();

            Assert.AreEqual(100, characterMaxHealth);
        }

        [Test]
        public void character_check_max_shields()
        {
            CharacterStats character = new CharacterStats();

            int characterMaxShields = character.getCharacterMaxShields();

            Assert.AreEqual(50, characterMaxShields);
        }

        [Test]
        public void character_is_not_dead()
        {
            CharacterStats character = new CharacterStats();
            
            Assert.IsFalse(character.isDead);
        }

        [Test]
        public void character_check_amount_damage()
        {
            PlayerCombat character = new PlayerCombat();

            Assert.AreEqual(10, character.attackDamage);
        }
    }
}
