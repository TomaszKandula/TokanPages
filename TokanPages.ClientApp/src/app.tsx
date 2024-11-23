import * as React from "react";
import { BrowserRouter, Switch } from "react-router-dom";
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import { GetContentManifestDto } from "./Api/Models";
import { UpdateUserData } from "./Shared/Services/initializeService";
import { HasSnapshotMode } from "./Shared/Services/SpaCaching";
import { GetDefaultId, UpdateUserLanguage } from "./Shared/Services/languageService";
import { Routes } from "./routes";
import AOS from "aos";
import "aos/dist/aos.css";
import {
    ClearPageStart,
    ScrollToTop,
    ApplicationToast,
    ApplicationDialogBox,
    ApplicationUserInfo,
    ApplicationSession,
} from "./Shared/Components";

interface Properties {
    manifest: GetContentManifestDto | undefined;
}

const App = (props: Properties): React.ReactElement => {
    const hasSnapshotMode = HasSnapshotMode();

    UpdateUserData();
    UpdateUserLanguage(props.manifest);

    React.useEffect(() => {
        AOS.init({ once: !hasSnapshotMode, disable: hasSnapshotMode });
        const intervalId = setInterval(() => AOS.refresh(), 900);
        return () => clearInterval(intervalId);
    }, [hasSnapshotMode]);

    React.useEffect(() => {
        if (props.manifest?.languages === undefined) {
            return;
        }

        if (window.location.pathname === "/") {
            const defaultId = GetDefaultId(props.manifest.languages);
            window.location.pathname = `/${defaultId}`;
        }
    }, [window.location.pathname]);

    return (
        <ApplicationSession>
            <BrowserRouter>
                <ClearPageStart>
                    <Switch>
                        <Routes languages={props.manifest?.languages} />
                    </Switch>
                </ClearPageStart>
            </BrowserRouter>
            <ApplicationToast />
            <ApplicationDialogBox />
            <ApplicationUserInfo />
            <ScrollToTop>
                <Fab size="small" aria-label="scroll back to top" className="button-up">
                    <KeyboardArrowUpIcon />
                </Fab>
            </ScrollToTop>
        </ApplicationSession>
    );
};

export default App;
