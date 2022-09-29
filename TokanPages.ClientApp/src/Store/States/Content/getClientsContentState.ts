import { IClientsContentDto } from "../../../Api/Models";

export interface IGetClientsContent extends IClientsContentDto
{ 
    isLoading: boolean;
}