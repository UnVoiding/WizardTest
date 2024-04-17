using UnityEngine;


namespace Romeno.WizardTest
{
    public class MainCharacter : MonoBehaviour
    {
        // REFERENCES
        [SerializeField] 
        private Rigidbody Rigidbody;
        [SerializeField]
        public Camera CharacterViewCamera;
        [SerializeField]
        private Transform SpellSpawnTransform;

        // RUNTIME
        [HideInInspector]
        public float MaxHealth;
        [HideInInspector]
        public float Health;
        
        private SpellType SelectedSpell;

        private float LastAttackTime;

        // cached
        [SerializeField]
        public SpellData SelectedSpellData;

        public void Init()
        {
            MaxHealth = DB.I.RoundSettings.WizardSettings.MaxHealth;

            SetSelectedSpell(SpellType.Fireball);
            SetHealth(MaxHealth);

            CameraManager.I.ActivateCharacterCamera();
        }

        public void SetSelectedSpell(SpellType spellType)
        {
            SelectedSpell = spellType;
            SelectedSpellData = DB.I.RoundSettings.WizardSettings.AvailableSpells.GetSpellData(SelectedSpell);

            UIManager.I.GetWidget<HUD>().SetSelectedSpellName(SelectedSpellData.Name);
        }
        
        public void SetHealth(float health)
        {
            Health = health;
            UIManager.I.GetWidget<HUD>().SetHealth(Health / MaxHealth);

            if (Health <= 0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            RoundManager.I.OnPlayerKilled();
        }

        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            Rigidbody.velocity = transform.forward * Input.GetAxis("Vertical") * DB.I.RoundSettings.WizardSettings.MoveSpeed;
            Rigidbody.angularVelocity = transform.up * Input.GetAxis("Horizontal") * DB.I.RoundSettings.WizardSettings.TurnRate;
        }

        private void HandleInput()
        {
            if (Input.GetButton("Attack"))
            {
                HandleAttack();
            }
            else if (Input.GetButtonDown("PreviousSpell"))
            {
                HandleChangeSpell(-1);
            }
            else if (Input.GetButtonDown("NextSpell"))
            {
                HandleChangeSpell(1);
            }
        }

        private void HandleAttack()
        {
            if (Time.time - LastAttackTime >= DB.I.RoundSettings.WizardSettings.AttackPeriod)
            {
                LastAttackTime = Time.time;
                
                SpellData data = DB.I.RoundSettings.WizardSettings.AvailableSpells.GetSpellData(SelectedSpell);

                SpellProjectile spell = PoolManager.I.Spells.Get(data.Prefab.gameObject);
                spell.gameObject.transform.position = SpellSpawnTransform.position;
                spell.gameObject.transform.rotation = SpellSpawnTransform.rotation;
            
                spell.Init(data);
            }
        }

        private void HandleChangeSpell(int offset)
        {
            SpellType newSelectedSpell = EnumCache.GetSpellTypeIndexCyclical(SelectedSpell, offset);

            SetSelectedSpell(newSelectedSpell);
        }

        public void TakeDamage(float damage)
        {
            if (damage > DB.I.RoundSettings.WizardSettings.Defence)
            {
                SetHealth(Health - damage - DB.I.RoundSettings.WizardSettings.Defence);
            }
        }
        
        public void ActivateCharacterCamera(bool value)
        {
            CharacterViewCamera.gameObject.SetActive(value);
            if (value)
            {
                CharacterViewCamera.gameObject.tag = "MainCamera";
            }
            else
            {
                CharacterViewCamera.gameObject.tag = "Untagged";
            }
        }
    }
}