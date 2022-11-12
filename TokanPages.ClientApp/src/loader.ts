import { IGetContentManifestDto } from "./Api/Models";
import { ApiCall, EnrichConfiguration } from "./Api/Request";
import { GET_CONTENT_MANIFEST } from "./Api/Request";

export const Initialize = async (callback: any): Promise<void> => 
{
    const result = await ApiCall(EnrichConfiguration(
    {
        url: GET_CONTENT_MANIFEST,
        method: "GET",
        responseType: "json"
    }));

    const content: IGetContentManifestDto = result.content as IGetContentManifestDto;
    callback(content);
}
