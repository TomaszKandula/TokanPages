import * as React from "react";
import { useDispatch } from "react-redux";
import Icon from "@mdi/react";
import * as Icons from "@mdi/js";
import { ItemDto, SubitemDto } from "../../../../../../../Api/Models";
import { ApplicationNavbarAction } from "../../../../../../../Store/Actions";
import { ReactMouseEventHandler } from "../../../../../../../Shared/types";
import { Link } from "../../../../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import "./renderNavbarItem.css";

interface NavbarItemWithoutSubitemsProps extends ItemDto {
    selectionStyle: string | undefined;
    onMouseEnter: ReactMouseEventHandler;
}

interface NavbarItemWithSubitemsProps extends ItemDto {
    selectionStyle: string | undefined;
}

const getIconSvgPath = (item: string | undefined): string => {
    const key = `mdi${item ?? ""}`;
    // @ts-expect-error
    const svg = Icons[key];
    return svg;
};

const NavbarItemWithSubitems = (props: NavbarItemWithSubitemsProps): React.ReactElement => (
    <div className="bulma-navbar">
        <div className="bulma-navbar-item bulma-has-dropdown bulma-is-hoverable">
            <a className={`bulma-navbar-link ${props.selectionStyle}`}>{props.value}</a>
            <div className="bulma-navbar-dropdown bulma-is-boxed bulma-is-right">
                {props.subitems?.map((item: SubitemDto, _index: number) => (
                    <Link
                        className="bulma-navbar-item"
                        key={uuidv4()}
                        to={item.link as string}
                        isDisabled={!item.enabled}
                    >
                        <Icon path={getIconSvgPath(item.icon)} size={0.8} />
                        <span>{item.value}</span>
                    </Link>
                ))}
            </div>
        </div>
    </div>
);

const NavbarItemWithoutSubitems = (props: NavbarItemWithoutSubitemsProps): React.ReactElement => (
    <Link
        key={props.id}
        to={props.link as string}
        className="render-navbar-list-item"
        onMouseEnter={props.onMouseEnter}
        isDisabled={!props.enabled}
    >
        <span className={props.selectionStyle}>{props.value}</span>
    </Link>
);

const selectionClass = "render-navbar-list-item-text render-navbar-list-item-text-selected";
const selectionBase = "render-navbar-list-item-text";

export const RenderNavbarItem = (props: ItemDto): React.ReactElement => {
    const dispatch = useDispatch();

    const [isSelected, setIsSelected] = React.useState(false);
    const [selectionStyle, setSelectionStyle] = React.useState<string>(selectionBase);

    const hasSubitems = props.subitems !== undefined && props.subitems.length > 0;

    React.useEffect(() => {
        const isNotRootPath = window.location.pathname !== "/";
        const pathname = window.location.pathname;

        if (hasSubitems) {
            const nomilized = props.value.toLowerCase();
            setIsSelected(isNotRootPath && pathname.includes(nomilized));
        } else {
            setIsSelected(isNotRootPath && pathname === props.link);
        }
    }, [props.value, props.link, props.subitems, hasSubitems]);

    React.useEffect(() => {
        setSelectionStyle(isSelected ? selectionClass : selectionBase);
    }, [isSelected]);

    const onMouseEnter = React.useCallback(() => {
        dispatch(ApplicationNavbarAction.set({ selection: props.value }));
    }, [props.value]);

    return hasSubitems ? (
        <NavbarItemWithSubitems {...props} selectionStyle={selectionStyle} />
    ) : (
        <NavbarItemWithoutSubitems {...props} selectionStyle={selectionStyle} onMouseEnter={onMouseEnter} />
    );
};
