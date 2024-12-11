import * as React from "react";
import { Button, ClickAwayListener, Grow, ListItemText, MenuList, Popper } from "@material-ui/core";
import { ReactMouseEventButton } from "../../../../../Shared/types";
import { Item } from "../../Models";
import { EnsureDefinedExt } from "../EnsureDefined";
import { RenderSubitem } from "../RenderSubitem/renderSubitem";

type MouseMovement = "down" | "up" | "right" | "left";

interface ItemsProps extends Item {
    onClickEvent?: () => void;
}

const Items = (props: ItemsProps): React.ReactElement => {
    const data = props.subitems?.map(item => (
        <RenderSubitem
            key={item.id}
            id={item.id}
            type={item.type}
            value={item.value}
            link={item.link}
            icon={item.icon}
            enabled={item.enabled}
            indent={false}
            navbar={true}
            onClickEvent={props.onClickEvent}
        />
    ));

    return <>{data}</>;
};

const GetMouseMovement = (event: ReactMouseEventButton): MouseMovement | undefined => {
    if (event.movementY > 0 && event.movementX == 0) {
        return "down";
    } else if (event.movementY < 0 && event.movementX == 0) {
        return "up";
    } else if (event.movementX > 0 && event.movementY == 0) {
        return "right";
    } else if (event.movementX < 0 && event.movementY == 0) {
        return "left";
    }

    return undefined;
}

export const RenderNavbarItemSpan = (props: Item): React.ReactElement => {
    const [isOpen, setOpen] = React.useState(false);
    const [movement, setMovement] = React.useState<MouseMovement | undefined>(undefined);
    const anchorRef = React.useRef<HTMLButtonElement>(null);

    const isSelected = window.location.pathname !== "/" && window.location.pathname === props.link;
    const selectionClass = "render-navbar-list-item-text render-navbar-list-item-text-selected";
    const selectionStyle = isSelected ? selectionClass : "render-navbar-list-item-text";

    const handleToggle = React.useCallback((): void => {
        setOpen(!isOpen);
    }, []);

    const movementHandler = React.useCallback((event: ReactMouseEventButton): void => {
        const direction = GetMouseMovement(event);
        setMovement(direction);
    }, []);

    const leaveHandler = React.useCallback(() => {
        if (movement === "left" || movement === "right") {
            setOpen(false);
        }
    }, [movement]);

    const handleClose = React.useCallback(
        (event: React.MouseEvent<EventTarget>): void => {
            if (anchorRef.current && anchorRef.current.contains(event.target as HTMLElement)) {
                return;
            }

            setOpen(false);
        },
        [anchorRef.current]
    );

    const handleListKeyDown = React.useCallback((event: React.KeyboardEvent): void => {
        if (event.key === "Tab") {
            event.preventDefault();
            setOpen(false);
        }
    }, []);

    const checkItems = EnsureDefinedExt(
        {
            values: [props.link, props.icon, props.enabled],
            messages: [
                "Cannot render. Missing 'link' property.",
                "Cannot render. Missing 'icon' property.",
                "Cannot render. Missing 'enabled' property.",
            ],
        },
        <Items {...props} />
    );

    if (checkItems.hasErrors || checkItems.hasWarnings) {
        return <div>Cannot render.</div>;
    }

    return (
        <div>
            <Button
                key={props.id}
                ref={anchorRef}
                disabled={!props.enabled}
                onMouseOver={handleToggle}
                onMouseLeave={leaveHandler}
                onMouseMove={movementHandler}
                className="render-navbar-button"
                disableRipple={true}
            >
                <ListItemText
                    ref={anchorRef}
                    primary={props.value}
                    className={selectionStyle}
                    disableTypography={true}
                />
            </Button>
            <Popper 
                open={isOpen} 
                anchorEl={anchorRef.current} 
                role={undefined} 
                onMouseLeave={handleClose}
                transition 
                disablePortal 
            >
                {({ TransitionProps, placement }) => (
                    <Grow
                        {...TransitionProps}
                        style={{ transformOrigin: placement === "bottom" ? "center top" : "center bottom" }}
                    >
                        <div className="render-navbar-menu-box">
                            <ClickAwayListener onClickAway={handleClose}>
                                <MenuList
                                    autoFocusItem={isOpen}
                                    id="menu-list-grow"
                                    onKeyDown={handleListKeyDown}
                                    className="render-navbar-menu-list"
                                >
                                    <Items {...props} />
                                </MenuList>
                            </ClickAwayListener>
                        </div>
                    </Grow>
                )}
            </Popper>
        </div>
    );
};
