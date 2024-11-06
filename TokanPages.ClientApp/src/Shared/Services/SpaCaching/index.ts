import { ApplicationState } from "../../../Store/Configuration";

const SNAPSHOT_STATE = "snapshot-state";

export const HasSnapshotMode = (): boolean => {
    const queryParam = new URLSearchParams(window.location.search);
    const param = queryParam.get("mode");
    return param === "snapshot";
}

export const TrySnapshotState = (state: ApplicationState): void => {
    const hasSnapshotMode = HasSnapshotMode();
    if (hasSnapshotMode) {
        const serialized = JSON.stringify(state);
        const current = document.querySelector(`meta[name=\"${SNAPSHOT_STATE}\"]`);
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
