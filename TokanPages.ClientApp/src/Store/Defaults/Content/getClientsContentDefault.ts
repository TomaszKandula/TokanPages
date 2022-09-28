import { IGetClientsContent } from "../../States/Content/getClientsContentState";

export const GetClientsContentDefault: IGetClientsContent =
{
    isLoading: false,
    content: 
    {
        language: "",
        caption: "",
        images: []
    }
}
