import { ApplicationState } from "../../../Store/Configuration";

const SNAPSHOT_STATE = "snapshot-state";
const HANDLER = `meta[name=\"${SNAPSHOT_STATE}\"]`;

export const HasSnapshotMode = (): boolean => {
    const queryParam = new URLSearchParams(window.location.search);
    const param = queryParam.get("mode");
    return param === "snapshot";
}

export const GetSnapshotState = (): ApplicationState | undefined => {
    const meta = document.querySelector(HANDLER);
    if (meta !== null) {
        const content = meta.getAttribute("content");
        if (content === null) {
            return undefined;
        }

        const state = JSON.parse(content) as ApplicationState;
        meta.removeAttribute(HANDLER);
        return state;
    }

    return undefined;
}

export const TrySnapshotState = (state: ApplicationState): void => {
    const hasSnapshotMode = HasSnapshotMode();
    if (hasSnapshotMode) {
        const serialized = JSON.stringify(state).replaceAll("\"", "'");
        const current = document.querySelector(HANDLER);
        if (current !== null) {
            current.setAttribute("content", serialized);
        } else {
            const meta = document.createElement("meta");
            meta.name = SNAPSHOT_STATE;
            meta.content = serialized;
            document.head.appendChild(meta);
        }
    }
}
