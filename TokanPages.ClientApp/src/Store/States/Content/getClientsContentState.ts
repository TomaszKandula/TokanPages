import { IClientsContentDto } from "../../../Api/Models";

export interface IContentClients extends IClientsContentDto
{ 
    isLoading: boolean;
}