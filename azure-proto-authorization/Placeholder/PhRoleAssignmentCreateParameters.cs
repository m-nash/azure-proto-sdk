using Azure.ResourceManager.Authorization.Models;

namespace azure_proto_authorization
{
    public class PhRoleAssignmentCreateParameters
    {
        public PhRoleAssignmentCreateParameters(string roleDefinitionId, string principalId)
        {
            RoleDefinitionId = roleDefinitionId;
            PrincipalId = principalId;
        }
        //
        // Summary:
        //     The role definition ID used in the role assignment.
        public string RoleDefinitionId { get; }
        //
        // Summary:
        //     The principal ID assigned to the role. This maps to the ID inside the Active
        //     Directory. It can point to a user, service principal, or security group.
        public string PrincipalId { get; }
        //
        // Summary:
        //     The principal type of the assigned principal ID.
        public PrincipalType? PrincipalType { get; set; }
        //
        // Summary:
        //     The delegation flag used for creating a role assignment.
        public bool? CanDelegate { get; set; }

        public RoleAssignmentCreateParameters ToModel()
        {
            var model = new RoleAssignmentCreateParameters(RoleDefinitionId, PrincipalId);
            model.PrincipalType = PrincipalType;
            model.CanDelegate = CanDelegate;
            return model;
        }
    }
}
