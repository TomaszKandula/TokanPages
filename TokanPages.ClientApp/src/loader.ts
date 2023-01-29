import { GetContentManifestDto } from "./Api/Models";
import { 
    RequestContract,
    ExecuteAsync, 
    GetConfiguration, 
    GET_CONTENT_MANIFEST
} from "./Api/Request";

const requestManifest: RequestContract = {
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
    const manifestDto: GetContentManifestDto = manifest.content as GetContentManifestDto;

    hidePreloader();
    callback(manifestDto);
}
