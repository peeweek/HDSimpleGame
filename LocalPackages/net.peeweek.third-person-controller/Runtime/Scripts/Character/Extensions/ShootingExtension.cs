using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ThirdPersonController
{
    public class ShootingExtension : CharacterExtension
    {
        public enum InputMode
        {
            Button,
            Axis
        }

        public enum FireMode
        {
            Single,
            Rate
        }

        [Header("Objects")]
        public GameObject WeaponPrefab;
        public GameObject WeaponAttachmentSourceBone;
        public GameObject WeaponAttachmentTargetBone;

        [Header("Input")]
        public InputMode mode = InputMode.Axis;
        public float AxisThreshold = 0.5f;
        public string FireInput = "Fire1";

        [Header("Fire")]
        public FireMode fireMode = FireMode.Rate;
        public float fireRate = 10.0f;

        [Header("Animation")]
        public bool ApplySpinePitch = true;

        // Private
        private Animator animator;
        private WeaponRig weaponRig;
        private Character character;

        private float ttl = 0.0f;

        public static readonly int SHOOTING = Animator.StringToHash("Shooting");
        public static readonly int PITCH = Animator.StringToHash("Pitch");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            character = GetComponent<Character>();

            var weapon = Instantiate(WeaponPrefab);
            weaponRig = weapon.GetComponent<WeaponRig>();
            weaponRig.AttachmentSource = WeaponAttachmentSourceBone;
            weaponRig.AttachmentTarget = WeaponAttachmentTargetBone;
        }

        public override void UpdateExtension(Character character)
        {
            bool firing = false;
            if (mode == InputMode.Button)
                firing = Input.GetButton(FireInput);
            else
                firing = Input.GetAxis(FireInput) > AxisThreshold;

            // Checks if not sprinting or falling/jumping
            firing = firing && !character.IsSprinting && character.IsGrounded;

            // Spine Pitch 
            float pitch;
            if(ApplySpinePitch)
            {
                // Calculate Pitch
                pitch = (character.VirtualCamera.LookAt.position - character.VirtualCamera.transform.position).normalized.y * 0.5f + 0.5f;
            }
            else
            {
                pitch = 0.5f;
            }

            // Sends state to animator
            animator.SetBool(SHOOTING, firing);
            animator.SetFloat(PITCH, pitch);

            if (!firing)
            {
                ttl = 0.0f;
            }
            else
            {
                if(ttl <= 0.0f)
                {
                    // Shoot
                    weaponRig.Fire();
                    ttl = 1.0f / fireRate;
                }
                else
                {
                    ttl -= Time.deltaTime;
                }
            }
        }
    }
}
