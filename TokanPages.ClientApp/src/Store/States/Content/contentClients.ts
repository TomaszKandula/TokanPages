import { ClientsContentDto } from "../../../Api/Models";

export interface ContentClientsState extends ClientsContentDto
{ 
    isLoading: boolean;
}