import * as React from "react";
import { Link } from "../../../../../../../Shared/Components";
import { ItemDto, SubitemDto } from "../../../../../../../Api/Models";
import "./renderSidemenuItem.css";

const SidemenuWithSubitems = (props: ItemDto): React.ReactElement => (
    <li>
        <a>
            {/* <Icon name={props.icon as string} size={1} className="" /> */}
            <span>{props.value}</span>
        </a>
        <ul className="bulma-menu-list">
            {props.subitems?.map((item: SubitemDto, _index: number) => (
                <li>
                    <Link
                        key={item.id}
                        to={item.link as string}
                        isDisabled={!item.enabled}
                    >
                        {/* <Icon name={item.icon as string} size={1} className="" /> */}
                        <span>{item.value}</span>
                    </Link>
                </li>
            ))}
        </ul>
    </li>
);

const SidemenuWithoutSubitems = (props: ItemDto): React.ReactElement => (
    <li>
        <Link key={props.id} to={props.link as string} isDisabled={!props.enabled}>
            {/* <Icon name={props.icon as string} size={1} className="" /> */}
            <span>{props.value}</span>
        </Link>
    </li>
);

export const RenderSidemenuItem = (props: ItemDto): React.ReactElement => { 
    const hasSubitems = props.subitems !== undefined && props.subitems.length > 0;
    return hasSubitems ? <SidemenuWithSubitems {...props} /> : <SidemenuWithoutSubitems {...props} />;
};
