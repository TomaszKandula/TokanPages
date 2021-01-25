interface IUpdateUserDto
{
    id: string;
    userAlias: string;
    isActivated: boolean;
    firstName: string;
    lastName: string;
    emailAddress: string;
}

export type { IUpdateUserDto }
