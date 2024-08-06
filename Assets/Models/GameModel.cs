using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trivia_1/Create Game Model")]
public class GameModel : ScriptableObject
{
    public List<CategoryModel> Categories;
}
