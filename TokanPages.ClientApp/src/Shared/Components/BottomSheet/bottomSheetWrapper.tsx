import React from "react";
import { BottomSheetProps } from "./Types";
import { GetRootElement } from "../../../Shared/Services/Utilities";
import { createPortal } from "react-dom";
import { BottomSheet } from "./bottomSheet";

export const BottomSheetWrapper = (props: BottomSheetProps): React.ReactElement => {
    const root = GetRootElement();

    return <>{createPortal(<BottomSheet {...props}>{props.children}</BottomSheet>, root)}</>;
};
