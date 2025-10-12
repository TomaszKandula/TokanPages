import { UseDimensionsResult } from "../../../../Shared/Hooks";

export const getWidthRatio = (media: UseDimensionsResult): number => {
    if (media.isMobile) {
        if (media.hasLandscape) {
            return 0.5;
        } else {
            return 0.75;
        }
    } else {
        if (media.hasLandscape) {
            return 0.33;
        } else {
            return 0.5;
        }
    }
};
