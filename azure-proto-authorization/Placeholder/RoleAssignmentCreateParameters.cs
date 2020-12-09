using Azure.ResourceManager.Authorization.Models;

namespace azure_proto_authorization
{
    public class RoleAssignmentCreateParameters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleDefinitionId"></param>
        /// <param name="principalId"></param>
        public RoleAssignmentCreateParameters(string roleDefinitionId, string principalId)
        {
            RoleDefinitionId = roleDefinitionId;
            PrincipalId = principalId;
        }

        /// <summary>
        /// The identifier of the role definition used in the assignment
        /// </summary>
        public string RoleDefinitionId { get; }

        /// <summary>
        /// The Object ID of the principal used in the assignment
        /// </summary>
        public string PrincipalId { get; }

        /// <summary>
        /// The type of the principal used in the assignment
        /// </summary>
        public PrincipalType? PrincipalType { get; set; }

        /// <summary>
        /// Specifies whether the principal can delegate privileges
        /// </summary>
        public bool? CanDelegate { get; set; }


        public Azure.ResourceManager.Authorization.Models.RoleAssignmentCreateParameters ToModel()
        {
            var model = new Azure.ResourceManager.Authorization.Models.RoleAssignmentCreateParameters(RoleDefinitionId, PrincipalId);
            model.PrincipalType = PrincipalType;
            model.CanDelegate = CanDelegate;
            return model;
        }
    }
}
