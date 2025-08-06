import * as React from "react";
import { Icon, Link } from "../../../../../../../Shared/Components";
import { ItemDto, SubitemDto } from "../../../../../../../Api/Models";

const SidemenuWithSubitems = (props: ItemDto): React.ReactElement => (
    <li key={props.id}>
        <div className="is-flex is-align-items-center my-2 ml-3">
            <Icon name={props.icon as string} size={1.3} className="has-text-grey-dark" />
            <a className="p-0 m-0">
                <p className="ml-2">{props.value}</p>
            </a>
        </div>
        <ul className="bulma-menu-list" style={{ marginLeft: 20 }}>
            {props.subitems?.map((item: SubitemDto, _index: number) => (
                <li key={item.id}>
                    <Link to={item.link as string} isDisabled={!item.enabled} className="is-flex">
                        <Icon name={item.icon as string} size={1.3} className="has-text-grey-dark" />
                        <p className="ml-2">{item.value}</p>
                    </Link>
                </li>
            ))}
        </ul>
    </li>
);

const SidemenuWithoutSubitems = (props: ItemDto): React.ReactElement => (
    <li key={props.id}>
        <Link to={props.link as string} isDisabled={!props.enabled} className="is-flex">
            <Icon name={props.icon as string} size={1.3} className="has-text-grey-dark" />
            <p className="ml-2">{props.value}</p>
        </Link>
    </li>
);

export const RenderSidemenuItem = (props: ItemDto): React.ReactElement => {
    const hasSubitems = props.subitems !== undefined && props.subitems.length > 0;
    return hasSubitems ? <SidemenuWithSubitems {...props} /> : <SidemenuWithoutSubitems {...props} />;
};
