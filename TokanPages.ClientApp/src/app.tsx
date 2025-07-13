import * as React from "react";
import { useSelector } from "react-redux";
import { BrowserRouter, Switch } from "react-router-dom";
import { ApplicationState } from "./Store/Configuration";
import { GetContentManifestDto, LanguageItemDto } from "./Api/Models";
import { HasSnapshotMode } from "./Shared/Services/SpaCaching";
import { useAnimation, useApplicationLanguage, useUserData, useXssWarning } from "./Shared/Hooks";
import { MapComponentsToRoutes } from "./routes";
import {
    ClearPageStart,
    ScrollToTop,
    ApplicationCookie,
    ApplicationDialogBox,
    ApplicationUserInfo,
    ApplicationSession,
    ApplicationToaster,
} from "./Shared/Components";

interface AppProps {
    manifest: GetContentManifestDto | undefined;
}

interface RenderApplicationProps {
    languages: LanguageItemDto[] | undefined;
}

const RenderApplication = (props: RenderApplicationProps): React.ReactElement => {
    const hasSnapshotMode = HasSnapshotMode();
    useXssWarning();

    return (
        <ApplicationSession>
            <BrowserRouter>
                <ClearPageStart>
                    <Switch>
                        <MapComponentsToRoutes languages={props.languages} />
                    </Switch>
                </ClearPageStart>
            </BrowserRouter>
            {hasSnapshotMode ? null : (
                <>
                    <ApplicationCookie />
                    <ApplicationToaster hasAutoClose={true} AutoCloseDurationSec={15} position="top-right" />
                    <ApplicationDialogBox />
                    <ApplicationUserInfo />
                    <ScrollToTop />
                </>
            )}
        </ApplicationSession>
    );
};

const PrerenderedWrapper = (): React.ReactElement => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    return <RenderApplication languages={language.languages} />;
};

const App = (props: AppProps): React.ReactElement => {
    useUserData();
    useAnimation();

    const { isNormalMode } = useApplicationLanguage(props.manifest);
    if (isNormalMode) {
        return <RenderApplication languages={props.manifest?.languages} />;
    } else {
        return <PrerenderedWrapper />;
    }
};

export default App;
