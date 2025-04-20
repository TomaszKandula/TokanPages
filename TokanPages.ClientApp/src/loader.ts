import { GetContentManifestDto } from "./Api/Models";
import { ExecuteApiActionProps, ExecuteApiAction, GET_CONTENT_MANIFEST } from "./Api";
import { IsPreRendered } from "./Shared/Services/SpaCaching";

const requestManifest: ExecuteApiActionProps = {
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

    const manifest = await ExecuteApiAction(requestManifest);
    const manifestDto: GetContentManifestDto = manifest.content as GetContentManifestDto;

    hidePreloader();
    callback(manifestDto);
};
