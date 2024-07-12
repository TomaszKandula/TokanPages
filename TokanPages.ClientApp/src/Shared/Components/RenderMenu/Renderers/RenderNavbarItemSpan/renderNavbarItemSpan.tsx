import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Button, ClickAwayListener, Grow, ListItemText, MenuList, Popper } from "@material-ui/core";
import { ApplicationState } from "../../../../../Store/Configuration";
import { ApplicationNavbarAction } from "../../../../../Store/Actions";
import { Item } from "../../Models";
import { EnsureDefinedExt } from "../EnsureDefined";
import { RenderSubitem } from "../RenderSubitem/renderSubitem";
import { RenderNavbarItemSpanStyle } from "./renderNavbarItemSpanStyle";

interface ItemsProps extends Item {
    onClickEvent?: () => void;
}

const Items = (props: ItemsProps): JSX.Element => {
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

export const RenderNavbarItemSpan = (props: Item): JSX.Element => {
    const classes = RenderNavbarItemSpanStyle();
    const dispatch = useDispatch();

    const [isOpen, setOpen] = React.useState(false);
    const anchorRef = React.useRef<HTMLButtonElement>(null);

    const selection = useSelector((state: ApplicationState) => state.applicationNavbar.selection);
    const isSelected = props.id === selection && window.location.pathname !== "/";

    const selectionClass = `${classes.list_item_text} ${classes.list_item_text_selected}`;
    const selectionStyle = isSelected ? selectionClass : classes.list_item_text;

    const onClickEvent = React.useCallback(() => {
        dispatch(ApplicationNavbarAction.set({ selection: props.id }));
    }, [props.id]);

    const handleToggle = React.useCallback((): void => {
        setOpen(prevOpen => !prevOpen);
    }, []);

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

    /* Return focus to the button when we transitioned from "!open" to "open" */
    const prevOpen = React.useRef(isOpen);
    React.useEffect(() => {
        if (prevOpen.current === true && isOpen === false) {
            anchorRef.current!.focus();
        }

        prevOpen.current = isOpen;
    }, [isOpen]);

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
                onClick={handleToggle}
                className={classes.button}
                disableRipple={true}
            >
                <ListItemText
                    ref={anchorRef}
                    primary={props.value}
                    className={selectionStyle}
                    disableTypography={true}
                />
            </Button>
            <Popper open={isOpen} anchorEl={anchorRef.current} role={undefined} transition disablePortal>
                {({ TransitionProps, placement }) => (
                    <Grow
                        {...TransitionProps}
                        style={{ transformOrigin: placement === "bottom" ? "center top" : "center bottom" }}
                    >
                        <Box className={classes.menu_box}>
                            <ClickAwayListener onClickAway={handleClose}>
                                <MenuList
                                    autoFocusItem={isOpen}
                                    id="menu-list-grow"
                                    onKeyDown={handleListKeyDown}
                                    className={classes.menu_list}
                                >
                                    <Items {...props} onClickEvent={onClickEvent} />
                                </MenuList>
                            </ClickAwayListener>
                        </Box>
                    </Grow>
                )}
            </Popper>
        </div>
    );
};
