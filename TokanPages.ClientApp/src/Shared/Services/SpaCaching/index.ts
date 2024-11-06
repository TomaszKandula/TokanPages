import { ApplicationState } from "../../../Store/Configuration";

export const HasSnapshotMode = (): boolean => {
    const queryParam = new URLSearchParams(window.location.search);
    const param = queryParam.get("mode");
    return param === "snapshot";
}

export const TrySnapshotState = (state: ApplicationState): void => {
    if (HasSnapshotMode()) {
        const serialized = JSON.stringify(state);
        const meta = document.createElement("meta");

        meta.name = "snapshot-state";
        meta.content = serialized;

        document.head.appendChild(meta);
    }
}
