import { IGetContentManifestDto } from "./Api/Models";
import { ApiCall, EnrichConfiguration } from "./Api/Request";
import { API_QUERY_GET_CONTENT_MANIFEST } from "./Shared/constants";

export const Initialize = async (callback: any): Promise<void> => 
{
    const result = await ApiCall(EnrichConfiguration(
    {
        url: API_QUERY_GET_CONTENT_MANIFEST,
        method: "GET",
        responseType: "json"
    }));

    const content: IGetContentManifestDto = result.content as IGetContentManifestDto;
    callback(content);
}
