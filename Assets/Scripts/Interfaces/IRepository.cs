using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepository<Entity>
{
    bool SaveIfNotExists(Entity entity);
    int Delete(Entity entity);
    bool Exists(Entity entity);
    List<Entity> GetAll();
    bool Update(Entity entity);
    long Count();
}
