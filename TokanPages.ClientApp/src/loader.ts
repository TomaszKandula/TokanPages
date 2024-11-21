import { GetContentManifestDto } from "./Api/Models";
import { RequestContract, ExecuteAsync, GetConfiguration, GET_CONTENT_MANIFEST } from "./Api/Request";
import { IsPreRendered } from "./Shared/Services/SpaCaching";

const requestManifest: RequestContract = {
    configuration: {
        url: GET_CONTENT_MANIFEST,
        method: "GET",
        responseType: "json",
    },
};

const hidePreloader = (): void => {
    let preloader = document.getElementById("preloader");
    if (preloader !== null) preloader.style.display = "none";
};

export const Initialize = async (callback: (manifest: GetContentManifestDto | undefined) => void): Promise<void> => {
    if (IsPreRendered()) {
        callback(undefined);
        return;
    }

    const manifest = await ExecuteAsync(GetConfiguration(requestManifest));
    const manifestDto: GetContentManifestDto = manifest.content as GetContentManifestDto;

    hidePreloader();
    callback(manifestDto);
};
