namespace ProductTask.Shared.Enums
{
    public enum ErrorKey
    {
        None,
        InternalServerError,
        SomeFieldesIsRequired,
        UserNotFound,
        UserNameOrPasswordIsInvalid,
        UserNameHasBeenTaken,
        RoleNotFound,
        PLeaseChooseContents,
        SomeContentNotFound,
        CannotUpdateAdminRolePermission,
        TheRoleIsExist,
        ThisRoleCannotBeDeleted,
        ThereAreActiveUsersWithThisRoles,
        CategoryNotFound,
        ThereIsSomeCategoriesNotFound,
        ProductNotFound,
    }
}
