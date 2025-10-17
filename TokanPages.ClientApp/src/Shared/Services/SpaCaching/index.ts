import { ApplicationState } from "../../../Store/Configuration";
import { PRERENDER_PATH_PREFIX } from "../../../Shared/Constants";
import base64 from "base-64";
import utf8 from "utf8";

const SNAPSHOT_STATE = "snapshot-state";
const HANDLER = `meta[name=\"${SNAPSHOT_STATE}\"]`;

export const CleanupSnapshotMode = (url: string): string => {
    const hasSnapshotMode = HasSnapshotMode();
    if (hasSnapshotMode) {
        return url.replace("/snapshot", "");
    }

    return url;
};

export const IsPreRendered = (): boolean => {
    const root = document.getElementById("root");
    return root?.hasChildNodes() ?? false;
};

export const HasSnapshotMode = (): boolean => {
    const location = window.location.href;
    const normalized = location.toLocaleLowerCase();
    return normalized.includes(PRERENDER_PATH_PREFIX);
};

export const TryGetStateSnapshot = (): ApplicationState | undefined => {
    const meta = document.querySelector(HANDLER);
    if (meta !== null) {
        const content = meta.getAttribute("content");
        if (content === null) {
            return undefined;
        }

        const decoded = base64.decode(content);
        const text = utf8.decode(decoded);
        const state = JSON.parse(text) as ApplicationState;
        return state;
    }

    return undefined;
};

export const TryPostStateSnapshot = (state: ApplicationState): void => {
    const hasSnapshotMode = HasSnapshotMode();
    if (hasSnapshotMode) {
        const serialized = JSON.stringify(state);
        const encoded = utf8.encode(serialized);
        const data = base64.encode(encoded);
        const current = document.querySelector(HANDLER);
        if (current !== null) {
            current.setAttribute("content", data);
        } else {
            const meta = document.createElement("meta");
            meta.name = SNAPSHOT_STATE;
            meta.content = data;
            document.head.appendChild(meta);
        }
    }
};
