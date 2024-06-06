

const Header: React.FC = () => {

  return (
    <div className="div-global-header">
      <div>
      <img className="img-global-header" src="https://store.akamai.steamstatic.com/public/shared/images/header/logo_steam.svg?t=962016" alt="Ссылка на домашнюю страницу Steam"></img>
      </div>
      <div>
        <ul className="ul-global-header">
          <li className="li-global-header">Магазин</li>
          <li className="li-global-header">Спільнота</li>
          <li className="li-global-header">Player</li>
          <li className="li-global-header">Чат</li>
          <li className="li-global-header">Підтримка</li>
        </ul>
      </div>
    </div>
  )

}

export default Header;