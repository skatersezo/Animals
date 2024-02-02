namespace Animals.Core.Ports.Exceptions;

public class PigeonNotFoundException(int id) : Exception($"Pigeon with id {id} not found");

public class DogNotFoundException(int id) : Exception($"Dog with id {id} not found");

public class CatNotFoundException(int id) : Exception($"Cat with id {id} not found");

public class AnimalNotFoundException(int id) : Exception($"Animal with id {id} not found");