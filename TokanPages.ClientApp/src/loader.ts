import { IGetContentManifestDto } from "./Api/Models";
import { 
    IRequest,
    ExecuteAsync, 
    GetConfiguration, 
    GET_CONTENT_MANIFEST
} from "./Api/Request";

const requestManifest: IRequest = {
    configuration: {
        url: GET_CONTENT_MANIFEST,
        method: "GET",
        responseType: "json"
    }
}

const hidePreloader = () => 
{
    let preloader = document.getElementById("preloader");
    if (preloader !== null) preloader.style.display = "none";
}

export const Initialize = async (callback: any): Promise<void> => 
{
    const manifest = await ExecuteAsync(GetConfiguration(requestManifest));
    const manifestDto: IGetContentManifestDto = manifest.content as IGetContentManifestDto;

    hidePreloader();
    callback(manifestDto);
}
