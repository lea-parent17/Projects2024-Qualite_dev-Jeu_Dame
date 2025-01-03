namespace CheckerGame.Models;

public class Board
{
    /// <summary>
    /// Taille de la carte (nombre de cases par côté)
    /// </summary>
    public const int MapSize = 8;

    /// <summary>
    /// Taille d'une cellule en pixels
    /// </summary>
    public const int CellSize = 50;

    /// <summary>
    /// Carte du jeu représentant l'état de chaque case (0 = vide, 1 = joueur 1, 2 = joueur 2)
    /// </summary>
    public int[,]? GameMap { get; set; }

    /// <summary>
    /// Boutons correspondant aux cases de la carte de jeu
    /// </summary>
    public Button[,] GameButtons { get; set; }

    /// <summary>
    /// Initialise une nouvelle instance du plateau de jeu
    /// </summary>
    public Board()
    {
        InitializeBoard();
    }

    /// <summary>
    /// Initialise la carte et les boutons du jeu avec leurs états de départ
    /// </summary>
    public void InitializeBoard()
    {
        // Initialisation de la carte de jeu avec les positions de départ
        GameMap = new int[MapSize, MapSize]
        {
            { 0,1,0,1,0,1,0,1 },
            { 1,0,1,0,1,0,1,0 },
            { 0,1,0,1,0,1,0,1 },
            { 0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0 },
            { 2,0,2,0,2,0,2,0 },
            { 0,2,0,2,0,2,0,2 },
            { 2,0,2,0,2,0,2,0 }
        };

        // Initialisation des boutons associés à la carte de jeu
        GameButtons = new Button[MapSize, MapSize];
    }

    /// <summary>
    /// Vérifie si une position donnée est à l'intérieur des limites du plateau de jeu
    /// </summary>
    /// <param name="x">Coordonnée horizontale (colonne)</param>
    /// <param name="y">Coordonnée verticale (ligne)</param>
    /// <returns>True si la position est à l'intérieur des limites, sinon False</returns>
    public bool IsInsideBorders(int x, int y)
    {
        return x >= 0 && x < MapSize && y >= 0 && y < MapSize;
    }
}