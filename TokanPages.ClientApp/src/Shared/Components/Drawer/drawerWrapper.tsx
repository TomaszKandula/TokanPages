import React from "react";
import { DrawerProps } from "./Types";
import { GetRootElement } from "../../../Shared/Services/Utilities";
import { createPortal } from "react-dom";
import { Drawer } from "./drawer";

export const DrawerWrapper = (props: DrawerProps): React.ReactElement => {
    const root = GetRootElement();

    return <>{createPortal(<Drawer {...props}>{props.children}</Drawer>, root)}</>;
};
