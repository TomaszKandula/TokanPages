import { GetContentManifestDto } from "./Api/Models";
import { ExecuteActionRequest, ExecuteAsync, GET_CONTENT_MANIFEST } from "./Api/Request";
import { IsPreRendered } from "./Shared/Services/SpaCaching";

const requestManifest: ExecuteActionRequest = {
    url: GET_CONTENT_MANIFEST,
    configuration: {
        method: "GET",
        hasJsonResponse: true,
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

    const manifest = await ExecuteAsync(requestManifest);
    const manifestDto: GetContentManifestDto = manifest.content as GetContentManifestDto;

    hidePreloader();
    callback(manifestDto);
};
