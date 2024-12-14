import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Button, ClickAwayListener, Grow, ListItemText, MenuList, Popper } from "@material-ui/core";
import { ApplicationNavbarAction } from "../../../../../Store/Actions";
import { ApplicationState } from "../../../../../Store/Configuration";
import { Item } from "../../Models";
import { EnsureDefinedExt } from "../EnsureDefined";
import { RenderSubitem } from "../RenderSubitem/renderSubitem";

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

export const RenderNavbarItemSpan = (props: Item): React.ReactElement => {
    const dispatch = useDispatch();

    const [isOpen, setOpen] = React.useState(false);
    const anchorRef = React.useRef<HTMLButtonElement>(null);

    const selection = useSelector((state: ApplicationState) => state.applicationNavbar.selection);

    const nomilized = props.value.toLowerCase();
    const isSelected = window.location.pathname !== "/" && window.location.pathname.includes(nomilized);
    const selectionClass = "render-navbar-list-item-text render-navbar-list-item-text-selected";
    const selectionStyle = isSelected ? selectionClass : "render-navbar-list-item-text";

    React.useEffect(() => {
        if (selection !== props.value) {
            setOpen(false);
        }
    }, [selection, props.value]);

    const handleToggle = React.useCallback((): void => {
        setOpen(!isOpen);
        dispatch(ApplicationNavbarAction.set({ selection: props.value }));
    }, [props.value]);

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
                onMouseEnter={handleToggle}
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
