using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayIngredients.Interactions
{
    public class ThirdPersonInteractiveUser : InteractiveUser
    {
        public Transform ReferencePoint;
        public float InteractionAngle = 120.0f;

        public override bool CanInteract(Interactive interactive)
        {
            Vector3 toInteractive = (interactive.transform.position - ReferencePoint.transform.position).normalized;
            return Mathf.Acos(Vector3.Dot(toInteractive, ReferencePoint.transform.forward)) < Mathf.Deg2Rad * InteractionAngle * 0.5f;
        }

        public override Interactive[] SortCandidates(IEnumerable<Interactive> candidates)
        {
            return candidates.OrderBy(a => Vector3.Distance(a.gameObject.transform.position, this.transform.position)).ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = ReferencePoint.localToWorldMatrix;
            Gizmos.DrawFrustum(Vector3.zero, InteractionAngle, 0.1f, 2.0f, 1.0f);
        }
    }
}

